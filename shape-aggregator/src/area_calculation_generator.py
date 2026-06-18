from shapes.shape import Shape


def generate_results(shapes: list[Shape]):
    """
    Shows the total combined area of all the shapes. Both printed in the console but also in the file area.txt

    Arguments:
    shapes -- a list of shape objects
    """
    total_area = sum(shape.area() for shape in shapes)
    result = f"Total area: {total_area:.1f}"

    with open("area.txt", "w") as output_file:
        print(result)
        output_file.write(result + "\n")
