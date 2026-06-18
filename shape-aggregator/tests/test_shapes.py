import math

import pytest

from shapes.circle import Circle
from shapes.rectangle import Rectangle
from shapes.square import Square
from shapes.triangle import Triangle
from shapes.shape import Shape


class TestCircle:
    def test_area(self):
        assert Circle(radius=2).area() == pytest.approx(math.pi * 4)

    def test_zero_radius_has_zero_area(self):
        assert Circle(radius=0).area() == 0

    def test_is_a_shape(self):
        assert isinstance(Circle(radius=1), Shape)


class TestRectangle:
    def test_area(self):
        assert Rectangle(width=3, height=4).area() == pytest.approx(12)

    def test_is_a_shape(self):
        assert isinstance(Rectangle(width=1, height=1), Shape)


class TestSquare:
    def test_area(self):
        assert Square(side=5).area() == pytest.approx(25)

    def test_is_a_shape(self):
        assert isinstance(Square(side=1), Shape)


class TestTriangle:
    def test_area(self):
        assert Triangle(base=6, height=4).area() == pytest.approx(12)

    def test_is_a_shape(self):
        assert isinstance(Triangle(base=1, height=1), Shape)


def test_shape_is_abstract():
    """Shape defines an abstract ``area`` and cannot be instantiated directly."""
    with pytest.raises(TypeError):
        Shape()
