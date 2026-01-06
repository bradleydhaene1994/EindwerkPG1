using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Enumerations;
using CustomerSimulationBL.Interfaces;
using Microsoft.Identity.Client;

namespace CustomerSimulationBL.Services
{
    public class UploadService : IUploadService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly INameRepository _nameRepository;
        private readonly ICountryVersionRepository _countryVersionRepository;
        private readonly ICsvReader _csvReader;
        private readonly ITxtReader _txtReader;
        private readonly IJsonReader _jsonReader;
        public UploadService(IAddressRepository addressRepository, IMunicipalityRepository municipalityRepository, INameRepository nameRepository, ICountryVersionRepository countryVersionRepository, ICsvReader csvReader, ITxtReader txtReader, IJsonReader jsonReader)
        {
            _addressRepository = addressRepository;
            _municipalityRepository = municipalityRepository;
            _nameRepository = nameRepository;
            _countryVersionRepository = countryVersionRepository;
            _csvReader = csvReader;
            _txtReader = txtReader;
            _jsonReader = jsonReader;
        }
        private enum FileFormat
        {
            Csv,
            Json,
            Txt
        }
        private FileFormat GetFileFormat(string filePath)
        {
            return Path.GetExtension(filePath).ToLowerInvariant() switch
            {
                ".csv" => FileFormat.Csv,
                ".json" => FileFormat.Json,
                ".txt" => FileFormat.Txt,
                _ => throw new InvalidOperationException("Unsupported file format")
            };
        }
        public void Upload(string filePath, int year, UploadDataType dataType, int countryId, IProgress<int> progress, string countryName)
        {
            int countryVersionId = _countryVersionRepository.GetOrUploadCountryVersion(countryId, year);
            
            if(DataAlreadyExists(countryVersionId, dataType))
            {
                throw new Exception("Data of this type has already been uploaded for the selected country and year.");
            }

            FileFormat format = GetFileFormat(filePath);

            switch(dataType)
            {
                case UploadDataType.Address:
                    UploadAddresses(filePath, format, countryVersionId, progress);
                    break;

                case UploadDataType.FirstName:
                    UploadFirstNames(filePath, format, countryVersionId, progress, countryName);
                    break;

                case UploadDataType.LastName:
                    UploadLastNames(filePath, format, countryVersionId, progress, countryName);
                    break;

                case UploadDataType.Municipality:
                    UploadMunicipality(filePath, format, countryVersionId);
                    break;
            }
        }
        private void UploadAddresses(string filePath, FileFormat format, int countryVersionId, IProgress<int> progress)
        {
            var addresses = format switch
            {
                FileFormat.Csv => _csvReader.ReadAddresses(filePath).ToList(),
                FileFormat.Json => _jsonReader.ReadAddresses(filePath).ToList(),
                _ => throw new InvalidOperationException()
            };

            _addressRepository.UploadAddress(addresses, countryVersionId, progress);
        }
        private void UploadFirstNames(string filePath, FileFormat format, int countryVersionId, IProgress<int> progress, string countryName)
        {
            IEnumerable<FirstName> firstNames = format switch
            {
                FileFormat.Csv => _csvReader.ReadFirstNames(filePath),
                FileFormat.Json => _jsonReader.ReadFirstNames(filePath, countryName),
                FileFormat.Txt => _txtReader.ReadFirstNames(filePath, countryName),
                _ => throw new InvalidOperationException()
            };

            _nameRepository.UploadFirstName(firstNames, countryVersionId, progress);
        }
        private void UploadLastNames(string filePath, FileFormat format, int countryVersionId, IProgress<int> progress, string countryName)
        {
            IEnumerable<LastName> lastNames = format switch
            {
                FileFormat.Csv => _csvReader.ReadLastNames(filePath),
                FileFormat.Json => _jsonReader.ReadLastNames(filePath, countryName),
                FileFormat.Txt => _txtReader.ReadLastNames(filePath, countryName),
                _ => throw new InvalidOperationException()
            };

            _nameRepository.UploadLastName(lastNames, countryVersionId, progress);
        }
        private void UploadMunicipality(string filePath, FileFormat format, int countryVersionId)
        {
            IEnumerable<Municipality> municipalities = format switch
            {
                FileFormat.Json => _jsonReader.ReadMunicipalities(filePath),
                _ => throw new InvalidOperationException()
            };

            _municipalityRepository.UploadMunicipality(municipalities, countryVersionId);
        }
        public bool DataAlreadyExists(int countryVersionId, UploadDataType dataType)
        {
            return dataType switch
            {
                UploadDataType.FirstName => _nameRepository.HasFirstNames(countryVersionId),
                UploadDataType.LastName => _nameRepository.HasLastNames(countryVersionId),
                UploadDataType.Municipality => _municipalityRepository.HasMunicipalities(countryVersionId),
                UploadDataType.Address => _addressRepository.HasAddresses(countryVersionId),
                _ => false
            };
        }
    }
}
