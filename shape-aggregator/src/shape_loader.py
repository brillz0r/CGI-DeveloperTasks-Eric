import json
from shapes.circle import Circle
from shapes.rectangle import Rectangle
from shapes.triangle import Triangle
from shapes.square import Square

SHAPE_TYPES = {
    "circle": Circle,
    "rectangle": Rectangle,
    "triangle": Triangle,
    "square": Square,
}


def _create_shape(data):
    """
    Create a shape object depending on the type property from the data object.
    The possible shapes are defined in "SHAPE_TYPES"

    Arguments:
    data -- a data object representing a shape
    """

    if "type" not in data:
        raise ValueError("Shape is missing required property 'type'")

    shape_type = data["type"]

    shape_class = SHAPE_TYPES.get(shape_type)

    if shape_class is None:
        raise ValueError(f"Unknown shape type: {shape_type}")

    # filter out the "type" property
    shape_args = {k: v for k, v in data.items() if k != "type"}

    try:
        return shape_class(**shape_args)
    except TypeError as ex:
        raise ValueError(
            f"Invalid properties for shape type '{shape_type}': {ex}"
        ) from ex


def load_shapes(file_path: str):
    """
    Read file and create shape objects depending on the type property

    Arguments:
    file_path -- the path to the file to read from
    """

    try:
        with open(file_path, "r") as f:
            data = json.load(f)

    except FileNotFoundError:
        raise ValueError(f"File not found: {file_path}")

    except json.JSONDecodeError as ex:
        raise ValueError(
            f"Invalid JSON in '{file_path}' (line {ex.lineno}, column {ex.colno})"
        )

    return [_create_shape(item) for item in data]
