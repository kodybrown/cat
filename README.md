cat
===

A simple dos-based `cat` tool (similar to that of `cat` for Linux).

    SYNOPSIS:

      Displays the contents of the file(s) specified.

    USAGE:

      cat.exe [options] file [file...n]

      file      The name of the file to display. Enclose file names within quotes if it includes a space.

      -p        Pauses after each screenful (applies -pp).
      -pp       Pauses at the end.

      -l        Include line numbers in the output.
      -w        Nicely wrap lines longer than window width.

      -ib       Ignore blank lines.
      -ibw      Ignore blank and whitespace lines.
      -il:xyz   Ignore lines starting with 'xyz'.

      -f        Forces plain text display (ignores plugins).

      * You can reverse the effect of a flag, by prefixing it with a bang (!). This is useful when you need to override an environment variable.

    PLUGINS:

      --show-plugins    Displays all plugins that are available.

      Plugins must be in the same directory as cat.exe and must match the file pattern: `cat.*.dll`. See https://github.com/kodybrown/cat for more details.

    ENVIRONMENT VARIABLES:

      --show-envars     Displays all environment variables.

      These values can be set in your environment so you don't have to type them into the command-line every time you run `cat.exe`.

      To set a value, prefix the (short) command-line argument name with `cat`. The values are the same as you would use for the command-line.

      Examples:

        > SET cat_l=true
        > SET cat_w=true
        > SET cat_ib=true
        > SET cat_ibw=true
        > SET cat_il=xyz
        > SET cat_f=true

      * Only the examples displayed are considered valid environment variables.
      * Arguments specified on the command-line will always override environment variables.
      * Consult your operating system help for information on how to set environment variables for all sessions.

