import argparse


def get_arguments():
    """Defines and get the application arguments"""
    parser = argparse.ArgumentParser(
        description="Calculate areas for shapes in a JSON file."
    )

    parser.add_argument(
        "--file", required=True, help="Path to the JSON file containing shapes"
    )

    args = parser.parse_args()
    return args
