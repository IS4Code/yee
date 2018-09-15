yee
==========

`yee` is a command-line tool and an alternative to `tee`, available on Unix systems. I needed `tee` not to fail when one of the output files closes, and so `yee` uses good old `try`/`catch` and tries to redirect data to all outputs until either the input is closed, or there is no available output.

`yee` can be used for textual or binary data. There is no configuration.
