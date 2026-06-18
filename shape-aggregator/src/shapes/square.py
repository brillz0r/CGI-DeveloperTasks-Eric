from dataclasses import dataclass
from shapes.shape import Shape


@dataclass
class Square(Shape):
    side: float

    def area(self) -> float:
        """Calculates the total area of the square"""
        return self.side**2
