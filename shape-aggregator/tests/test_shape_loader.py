import json

import pytest

from shape_loader import load_shapes, _create_shape
from shapes.circle import Circle
from shapes.rectangle import Rectangle
from shapes.square import Square
from shapes.triangle import Triangle


def _write_json(tmp_path, data):
    """Write ``data`` as JSON to a temp file and return its path as a string."""
    file_path = tmp_path / "shapes.json"
    file_path.write_text(json.dumps(data))
    return str(file_path)


class TestCreateShape:
    def test_creates_each_known_shape_type(self):
        assert isinstance(_create_shape({"type": "circle", "radius": 1}), Circle)
        assert isinstance(
            _create_shape({"type": "rectangle", "width": 1, "height": 2}), Rectangle
        )
        assert isinstance(_create_shape({"type": "square", "side": 1}), Square)
        assert isinstance(
            _create_shape({"type": "triangle", "base": 1, "height": 2}), Triangle
        )

    def test_missing_type_raises(self):
        with pytest.raises(ValueError, match="missing required property 'type'"):
            _create_shape({"radius": 1})

    def test_unknown_type_raises(self):
        with pytest.raises(ValueError, match="Unknown shape type: hexagon"):
            _create_shape({"type": "hexagon", "side": 1})

    def test_missing_property_raises(self):
        with pytest.raises(ValueError, match="Invalid properties for shape type 'circle'"):
            _create_shape({"type": "circle"})

    def test_unexpected_property_raises(self):
        with pytest.raises(ValueError, match="Invalid properties for shape type 'square'"):
            _create_shape({"type": "square", "side": 1, "color": "red"})


class TestLoadShapes:
    def test_loads_multiple_shapes(self, tmp_path):
        path = _write_json(
            tmp_path,
            [
                {"type": "circle", "radius": 2},
                {"type": "square", "side": 3},
            ],
        )

        shapes = load_shapes(path)

        assert len(shapes) == 2
        assert isinstance(shapes[0], Circle)
        assert isinstance(shapes[1], Square)

    def test_empty_file_returns_empty_list(self, tmp_path):
        assert load_shapes(_write_json(tmp_path, [])) == []

    def test_missing_file_raises(self, tmp_path):
        missing = str(tmp_path / "does_not_exist.json")
        with pytest.raises(ValueError, match="File not found"):
            load_shapes(missing)

    def test_invalid_json_raises(self, tmp_path):
        path = tmp_path / "bad.json"
        path.write_text("{ not valid json ")
        with pytest.raises(ValueError, match="Invalid JSON"):
            load_shapes(str(path))

    def test_propagates_shape_errors(self, tmp_path):
        path = _write_json(tmp_path, [{"type": "circle"}])
        with pytest.raises(ValueError, match="Invalid properties"):
            load_shapes(path)
