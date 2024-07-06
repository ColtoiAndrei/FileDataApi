using FileDataApi.Enums;
using FileDataApi.Extensions;
using FileDataApi.Models;
using FileDataApi.Responses;

namespace FileDataApi.Services
{
    public class FileService : IFileService
    {
        public ProcessedFilesResponse ProcessFiles(List<IFormFile> files)
        {
            ProcessedFilesResponse response = new ();
            response.ValidFiles = new List<string>();
            response.InvalidFiles = new List<string>();
            response.Companies = new List<Company>();

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file.FileName);
                try
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        // Read entire file
                        string fileContent = reader.ReadToEnd();

                        // Determine file format
                        Delimiter delimiter = this.DetermineDelimiter(fileContent);

                        if (delimiter == Delimiter.Unknown)
                        {
                            response.InvalidFiles.Add(fileName);
                            continue;
                        }

                        List<string> lines = fileContent.SplitToLines();

                        List<Company> companiesFromFile = new List<Company>();
                        bool fileIsValid = true;

                        foreach (var line in lines)
                        {
                            if (string.IsNullOrWhiteSpace(line))
                            {
                                continue; // Skip empty lines and file is still valid
                            }

                            Company? company = CreateCompanyFromLine(line, delimiter);

                            if (company != null)
                            {
                                companiesFromFile.Add(company);
                            }
                            else
                            {
                                fileIsValid = false;
                                break;
                            }
                        }

                        if (fileIsValid)
                        {
                            response.ValidFiles.Add(fileName);
                            response.Companies.AddRange(companiesFromFile);
                        }
                        else
                        {
                            response.InvalidFiles.Add(fileName);
                        }
                    }
                }
                catch
                {
                    response.InvalidFiles.Add(fileName);
                }
            }
            return response;
        }

        private int CountOccurrences(string text, char delimiter)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (c == delimiter)
                {
                    count++;
                }
            }
            return count;
        }

        private Company? CreateCompanyFromLine(string line, Delimiter delimiter)
        {
            string[] fields;

            switch (delimiter)
            {
                case Delimiter.Comma:
                    fields = line.Split(',');
                    if (fields.Length != 5) //expected 5 fields on each line in comma-formated files
                        return null;

                    if (!int.TryParse(fields[3].Trim(), out int yearsInBusiness))
                        return null;

                    return new Company
                    {
                        CompanyName = fields[0].Trim(),
                        YearsInBusiness = yearsInBusiness,
                        ContactName = fields[1].Trim(),
                        ContactPhoneNumber = fields[2].Trim(),
                        ContactEmail = fields[4].Trim()
                    };

                case Delimiter.Hash:
                    fields = line.Split('#');
                    if (fields.Length != 4) //expected 4 fields on each line in comma-formated files
                        return null;

                    if (!int.TryParse(fields[1].Trim(), out int yearFounded))
                        return null;

                    return new Company
                    {
                        CompanyName = fields[0].Trim(),
                        YearsInBusiness = DateTime.Now.Year - yearFounded,
                        ContactName = fields[2].Trim(),
                        ContactPhoneNumber = fields[3].Trim(),
                        ContactEmail = string.Empty
                    };

                case Delimiter.Hyphen:
                    fields = line.Split('-');
                    if (fields.Length != 6) //expected 6 fields on each line in comma-formated files
                        return null;

                    if (!int.TryParse(fields[1].Trim(), out int yearFoundedHyphen))
                        return null;

                    return new Company
                    {
                        CompanyName = fields[0].Trim(),
                        YearsInBusiness = DateTime.Now.Year - yearFoundedHyphen,
                        ContactName = fields[4].Trim() + " " + fields[5].Trim(),
                        ContactPhoneNumber = fields[2].Trim(),
                        ContactEmail = fields[3].Trim(),
                    };

                default:
                    return null;
            }
        }

        private Delimiter DetermineDelimiter(string fileContent)
        {
            int commaCount = this.CountOccurrences(fileContent, ',');
            int hashCount = this.CountOccurrences(fileContent, '#');
            int hyphenCount = this.CountOccurrences(fileContent, '-');

            if (commaCount > hashCount && commaCount > hyphenCount)
            {
                return Delimiter.Comma;
            }
            else if (hashCount > commaCount && hashCount > hyphenCount)
            {
                return Delimiter.Hash;
            }
            else if (hyphenCount > commaCount && hyphenCount > hashCount)
            {
                return Delimiter.Hyphen;
            }
            else
            {
                // If counts are 0 or equal
                return Delimiter.Unknown;
            }
        }
    }
}
