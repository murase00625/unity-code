# Basic Unity 4.x 2D game scripts

This is a collection of Unity 4 scripts for 2D games.

## Platform game controllers
Very basic platforming game controllers for characters and enemies.
Moving platform support planned for later release.

### How to use
- Clone the repo or download as a zip
- Import the scripts to your project
- Player character:
-- Set the tag to "Player"
-- Create an object layer called Jumpable (Edit > Project Settings > Tags and Layers). Don't use sorting layers for this!
-- Assign all platforms, ground objects, and anything where jumping is desired to the Jumpable layer (top of the Inspector).
-- Add a controller script

## Coming soon
- Top-down controller

## License

The MIT License (MIT)

Copyright (c) 2015 Morio Murase & Makersville

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.