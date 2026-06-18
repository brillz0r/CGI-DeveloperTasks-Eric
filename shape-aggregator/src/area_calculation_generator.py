from shapes.shape import Shape


def generate_results(shapes: list[Shape]):
    """
    Shows the resulting areas of the shapes. Both printed in the console but also in the file area.txt

    Arguments:
    shapes -- a list of shape objects
    """
    with open("area.txt", "w") as output_file:
        for shape in shapes:
            result = f"Total area: {shape.area():.1f}"
            print(result)
            output_file.write(result + "\n")
