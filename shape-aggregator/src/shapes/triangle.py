from dataclasses import dataclass
from shapes.shape import Shape


@dataclass
class Triangle(Shape):
    base: float
    height: float

    def area(self) -> float:
        """Calculates the total area of the rectangle"""
        return 0.5 * self.base * self.height
