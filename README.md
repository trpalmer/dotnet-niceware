## Niceware for .NET
A .NET port of [Niceware](https://github.com/diracdeltas/niceware) for generating random-yet-memorable passwords. Each word provides 16 bits of entropy, so a useful password requires at least 3 words.

Because the wordlist is of exactly size 2^16, Niceware is also useful for converting cryptographic keys and other sequences of random bytes into human-readable phrases. With Niceware, a 128-bit key is equivalent to an 8-word phrase.

* Free software: MIT license
* Supports .NET Standard (core), .NET 4.6

## Usage

To Install:

Install via Nuget package manager:
```
PM> Install-Package niceware
```

To generate an 8-byte passphrase:

``` c#

using niceware;

var passphrase = Niceware.GeneratePassphrase(8);

// result: ["unpeopling", "whipsawing", "sought", "rune"]
```

## Credits
Niceware for .NET is a port of [Niceware](https://github.com/diracdeltas/niceware) by [yan](https://diracdeltas.github.io/blog/about/)