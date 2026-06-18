from dataclasses import dataclass
from shapes.shape import Shape
import math


@dataclass
class Circle(Shape):
    radius: float

    def area(self) -> float:
        """Calculates the total area of the circle"""
        return math.pi * self.radius**2
