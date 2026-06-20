from collections import Counter
from shapes.shape import Shape


def generate_results(shapes: list[Shape]):
    """
    Shows the total combined area of all the shapes along with a count per
    shape type. Both printed in the console but also in the file area.txt

    Arguments:
    shapes -- a list of shape objects
    """
    total_area = sum(shape.area() for shape in shapes)
    counts = Counter(type(shape).__name__ for shape in shapes)

    lines = [f"{name}: {count}" for name, count in counts.items()]
    lines.append(f"Total area: {total_area:.1f}")
    result = "\n".join(lines)

    with open("area.txt", "w") as output_file:
        print(result)
        output_file.write(result + "\n")
