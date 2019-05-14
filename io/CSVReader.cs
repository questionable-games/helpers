//
// Copyright (C) 2019 Alberto Pérez García-Plaza
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//
// Authors:
//     Alberto Pérez García-Plaza <alpgarcia@gmail.com>
//

using System.Collections.Generic;
using UnityEngine;

public static class CSVReader
{
    /// <summary>
    /// Read a CSV file located at the specified filepath (must be inside
    /// Resources folder).
    /// </summary>
    /// <returns>List of Dictionaries, being each one a CSV row.</returns>
    /// <param name="filepath">Filepath within Resources folder.</param>
    public static List<Dictionary<string, object>> Read(string filepath)
    {
        var entryList = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(filepath) as TextAsset;

        string[] lines = data.text.Split('\n');

        if (lines.Length <= 1)
        {
            return entryList;
        }

        string[] header = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            // Skip empty lines
            if (values.Length == 1 && values[0] == "") {
                continue;
            }

            if (header.Length != values.Length)
            {
                throw new CSVMissingValuesException(
                    "Header columns doesn't match values in line: " + (i+1) +
                    "(" + header.Length + "/" + values.Length + ")");
            }

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length; j++)
            {
                object entryValue;

                string strValue = values[j];
                int intValue;
                if (int.TryParse(strValue, out intValue))
                {
                    entryValue = intValue;
                }
                else
                {
                    entryValue = strValue;
                }

                entry[header[j]] = entryValue;
            }

            entryList.Add(entry);

        }

        return entryList;
    } 
}
