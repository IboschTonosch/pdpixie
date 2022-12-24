using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Internal;
using System.IO;

namespace pdpixie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> arguments = args.ToList();

            var fileNames = new Dictionary<FileType, HashSet<string>>();

            string targetFileName = "";

            PdConvert pdConvert = default;
            PdMerge pdMerge = default;

            if (arguments.Count == 0 || arguments.Contains("-h") || arguments.Contains("--help"))
            {
                System.Console.WriteLine(Help.Description);
                return;
            }

            while (true)
            {
                if (arguments.Exists(arg => arg.Equals("-c") || arg.Equals("--convert")))
                {
                    pdConvert = new PdConvert();
                    arguments.Remove("-c");
                    arguments.Remove("--convert");
                    continue;
                }

                if (arguments.Exists(arg => arg.Equals("-m") || arg.Equals("--merge")))
                {
                    pdMerge = new PdMerge();
                    arguments.Remove("-m");
                    arguments.Remove("--merge");
                    continue;
                }

                if (arguments.Exists(arg => arg.Equals("-s") || arg.Equals("--source")))
                {
                    if (pdConvert == null && pdMerge == null)
                    {
                        System.Console.WriteLine("Error: Option --convert or --merge is missing");
                        return;
                    }

                    int idx = arguments.FindIndex(arg => arg.Equals("-s") || arg.Equals("--source"));
                    while (arguments.Count > ++idx)
                    {
                        string fileName = arguments[idx];
                        if (fileName.Contains("-t"))
                        {
                            break;
                        }
                        if (File.Exists(fileName))
                        {
                            if (fileName.Contains(".jpg") || fileName.Contains("png"))
                            {
                                if (!fileNames.ContainsKey(FileType.IMAGE))
                                {
                                    fileNames[FileType.IMAGE] = new HashSet<string>();
                                }
                                fileNames[FileType.IMAGE].Add(fileName);
                            }
                            else if (fileName.Contains(".docx"))
                            {
                                if (!fileNames.ContainsKey(FileType.DOCX))
                                {
                                    fileNames[FileType.DOCX] = new HashSet<string>();
                                }
                                fileNames[FileType.DOCX].Add(fileName);
                            }
                            else if (fileName.Contains(".pdf"))
                            {
                                if (!fileNames.ContainsKey(FileType.PDF))
                                {
                                    fileNames[FileType.PDF] = new HashSet<string>();
                                }
                                fileNames[FileType.PDF].Add(fileName);
                            }
                            else
                            {
                                System.Console.WriteLine($"File {fileName} is not supported");
                                return;
                            }
                        }
                        else
                        {
                            System.Console.WriteLine($"File {fileName} does not exist");
                            return;
                        }
                    }

                    if (fileNames.Count == 0)
                    {
                        System.Console.WriteLine($"Error: At least one source file must me defined with the --source option");
                        return;
                    }

                    arguments.Remove("-s");
                    arguments.Remove("--source");
                    continue;
                }

                if (arguments.Exists(arg => arg.Equals("-t") || arg.Equals("--target")))
                {
                    if (pdConvert == null && pdMerge == null)
                    {
                        System.Console.WriteLine("Error: Option --convert or --merge is missing");
                        return;
                    }
                    if (fileNames.Count == 0)
                    {
                        System.Console.WriteLine("Error: At least one source file must me defined with the --source option");
                        return;
                    }

                    int idx = arguments.FindIndex(arg => arg.Equals("-t") || arg.Equals("--target"));
                    if (arguments.Count > ++idx)
                    {
                        string fileName = arguments[idx];
                        targetFileName = fileName;
                    }
                    else
                    {
                        System.Console.WriteLine("Error: The target filename argument is missing");
                        return;
                    }

                    arguments.Remove("-t");
                    arguments.Remove("--target");
                    continue;
                }

                break;
            }

            using (var mergedDocument = new PdfDocument())
            {
                if (pdConvert != default)
                {
                    pdConvert.ConvertFiles(mergedDocument, fileNames);
                }

                if (pdMerge != default)
                {
                    // save the document with its pages as it is
                    if (!string.IsNullOrEmpty(targetFileName))
                    {
                        mergedDocument.Save(targetFileName);
                    }
                    else
                    {
                        System.Console.WriteLine("Error: No target file name defined");
                    }
                }
                else
                {
                    // separate the pages from the document and save them in a separate document
                    foreach (PdfPage page in mergedDocument.Pages)
                    {
                        using (var document2 = new PdfDocument())
                        {
                            document2.AddPage((PdfPage)page.Clone());
                            document2.Save(page.Tag.ToString());
                        }
                    }
                }
            }
        }
    }
}
