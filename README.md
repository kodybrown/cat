cat
===

simple dos-based cat tool (similar to that of cat for Linux)


    cat.exe displays the contents of the file specified.

    USAGE: cat [options] file [file2] [file..n]

       -l   includes line numbers in the output
       -w   nicely wrap lines longer than window width
       -f   forces plain text display

       -ib       ignore blank lines
       -ibw      ignore blank and whitespace lines
       -il:xyz   ignore lines starting with 'xyz'

       note: enclose the filename within quotes if it includes a space.
