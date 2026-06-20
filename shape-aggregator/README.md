# Shape Aggregator

A small command-line tool that reads a list of geometric shapes from a JSON
file, counts how many of each shape type were provided, and sums their areas
into a single total. The results are reported both to the console and to an
`area.txt` file.

## Supported shapes

| Type        | Required properties | Area formula        |
|-------------|---------------------|---------------------|
| `circle`    | `radius`            | π × radius²         |
| `rectangle` | `width`, `height`   | width × height      |
| `triangle`  | `base`, `height`    | ½ × base × height   |
| `square`    | `side`              | side²               |

## Input format

The input is a JSON array where each object has a `type` property plus the
properties required by that shape. See [`src/shapes.json`](src/shapes.json) for
an example:

```json
[
  { "type": "circle",    "radius": 5 },
  { "type": "rectangle", "width": 3, "height": 4 },
  { "type": "triangle",  "base": 6, "height": 2 },
  { "type": "square",    "side": 4 }
]
```

Invalid input is reported with a clear error message, including: a missing file,
malformed JSON, an unknown shape type, or missing/extra properties for a shape.

## Requirements

- Python 3.9 or newer

The tool uses only the Python standard library, so no third-party packages are
required to run it. The only extra dependency is `pytest`, which is needed just
to run the test suite (see [Running the tests](#running-the-tests)).

## Getting started

1. (Optional) Create and activate a virtual environment:

   ```powershell
   python -m venv .venv
   .venv\Scripts\Activate.ps1
   ```

2. Run the tool against a JSON file of shapes:

   ```bash
   python src/main.py --file src/shapes.json
   ```

   Example output:

   ```
   Circle: 1
   Rectangle: 1
   Triangle: 1
   Square: 1
   Total area: 112.5
   ```

   A count is printed for each shape type present in the input, followed by the
   combined area of all shapes. The same lines are also written to `area.txt` in
   the current directory.

## Running the tests

The project uses [pytest](https://docs.pytest.org/), which is the only
dependency you need to install. A virtual environment is recommended to keep it
isolated, but not required — you can also install `pytest` globally (or with
`pip install --user pytest`).

```bash
pip install pytest
pytest
```

## Project structure

```
src/
  main.py                        Entry point: parses args, loads shapes, prints results
  argument_parser.py             Defines the --file command-line argument
  shape_loader.py                Reads the JSON file and builds shape objects
  area_calculation_generator.py  Counts shapes, totals their area, writes to area.txt
  shapes.json                    Example input file
  shapes/
    shape.py                     Abstract base class defining area()
    circle.py, rectangle.py, triangle.py, square.py
tests/                           pytest test suite
```
