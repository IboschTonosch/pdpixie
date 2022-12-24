# pdpixie

a very basic command line app to save selected images file into one or multiple pdf documents.

this console app uses the PDFSharp libraries.

There is not much code and the code ist not nicely structured. I just quickly put something together out of need.

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


