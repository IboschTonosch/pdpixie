# pdpixie

a very basic windows command line app to save selected images file into one or multiple pdf documents.

this console app uses 3th party PDFSharp library which is licenced under the MIT license

There is not much code and the code ist not nicely structured. I just quickly put something together out of need.

C# project
Developed with Visual Studio Community 2022

#########################################################################################################################################

Usage: pdpixie [options]... [File(s)]...

create / merge pdf files from images

  Option	Long option		Description
  -------------------------------------------------
  -c		--convert		converts selected files into pdf
  -s		--source		source filenames to be processed
  -m		--merge			merges multiple pdf files to one pdf file
  -t		--target		target filename (works only on one source file or when --merge option is added)


Examples:

  pdpixie -cm -s img1.jpg img2.jpg img2.jpg -t merged_images.pdf	Converts selected image files and merge them into one pdf file


