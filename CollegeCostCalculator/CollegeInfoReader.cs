using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;

namespace CollegeCostCalculator
{
    /// <summary>
    /// Reads college cost data from a json or csv file and provides access to this information
    /// through a dictonary of CollegeInfo.
    /// </summary>
    internal class CollegeInfoReader
    {
        private static Assembly _assembly = Assembly.GetAssembly(typeof(CollegeInfoReader));

        private static string JSON_FILE_OUT = "Data\\Colleges.json";

        private static string JSON_FILE = "CollegeCostCalculator.Data.Colleges.json";

        private static string CSV_FILE = "CollegeCostCalculator.Data.college_costs.csv";

        public Dictionary<string, CollegeInfo> Colleges { get; private set;}

        /// <summary>
        /// Creates a new CollegeInfoReader and tries to populate the Colleges dictionary with data.
        /// </summary>
        /// <exception>
        /// Thrown when the data does not exist or an error was encountered when reading a file.
        /// </exeption>
        public CollegeInfoReader() 
        {
            try
            {
                ReadJson();
            }
            catch (Exception)
            {
                ReadCSV();
            }
            finally
            {
                if (Colleges == null)
                {
                    throw new Exception("Could not read colleges data.");
                }
            }
        }

        /// <summary>
        /// Populates the Colleges dictionary with data from a json file if one exists.
        /// </summary>
        /// <remarks>
        /// Reads the json data specified by the embbedded resource JSON_FILE.
        /// </remarks>
        /// <exception name="FileNotFoundException">Thrown when json file does not exist.</exeption>
        private void ReadJson()
        {
            using (StreamReader reader = new StreamReader(_assembly.GetManifestResourceStream(JSON_FILE)))
            {
                var json = reader.ReadToEnd();
                var list = JsonConvert.DeserializeObject<List<CollegeInfo>>(json);
                Colleges = list.Select(x => new KeyValuePair<string, CollegeInfo>(x.Name, x))
                    .ToDictionary(y => y.Key, y => y.Value);
            }
        }

        /// <summary>
        /// Populates the Colleges dictionary with data from a csv file.
        /// Cleans the input and saves it to a json file (Colleges.json) for future use.
        /// </summary>
        /// <remarks>
        /// Reads the csv data specified by the embbedded resource CSV_FILE.
        /// </remarks>
        /// <exception name="FileNotFoundException">Thrown when csv file does not exist.</exeption>
        private void ReadCSV()
        {
            using (var parser = new TextFieldParser(_assembly.GetManifestResourceStream(CSV_FILE)))
            {
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");
                Colleges = new Dictionary<string, CollegeInfo>();
                var CollegeList = new List<CollegeInfo>();

                // Skip header
                if (!parser.EndOfData)
                {
                    parser.ReadFields();
                }

                while (!parser.EndOfData)
                {
                    var values = parser.ReadFields();

                    string name = values[0];
                    Decimal? tuitionIn = null;
                    Decimal? tuitionOut = null;
                    Decimal? roomBoard = null;

                    if (Decimal.TryParse(values[1], out decimal tIn))
                    {
                        tuitionIn = tIn;
                    }

                    if (Decimal.TryParse(values[2], out decimal tOut))
                    {
                        tuitionOut = tOut;
                    }

                    if (Decimal.TryParse(values[3], out decimal room))
                    {
                        roomBoard = room;
                    }

                    if (!(name == null || name.Length == 0))
                    {
                        name = name.Replace('�', ' ');
                        CollegeInfo info = new CollegeInfo(name, tuitionIn, tuitionOut, roomBoard);
                        Colleges.Add(name, info);
                        CollegeList.Add(info);
                    }
                }
                Directory.CreateDirectory("Data");
                File.WriteAllText(JSON_FILE_OUT, JsonConvert.SerializeObject(CollegeList, Formatting.Indented));
            }
        }
    }
}
