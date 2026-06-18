from dataclasses import dataclass
from shapes.shape import Shape


@dataclass
class Rectangle(Shape):
    width: float
    height: float

    def area(self) -> float:
        """Calculates the total area of the rectangle"""
        return self.width * self.height
