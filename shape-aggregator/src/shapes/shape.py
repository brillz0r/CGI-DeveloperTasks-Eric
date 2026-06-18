from abc import ABC, abstractmethod


class Shape(ABC):
    @abstractmethod
    def area(self):
        """Abstract method where the shape area should be calculated in the derived classes"""
        pass
