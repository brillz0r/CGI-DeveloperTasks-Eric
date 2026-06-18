import sys
from shape_loader import load_shapes
from argument_parser import get_arguments
from area_calculation_generator import generate_results


try:
    args = get_arguments()
    shapes = load_shapes(args.file)
    generate_results(shapes)

except ValueError as ex:
    print(f"Error: {ex}")
    sys.exit(1)
