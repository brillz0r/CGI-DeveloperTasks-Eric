from area_calculation_generator import generate_results
from shapes.square import Square
from shapes.rectangle import Rectangle


def test_prints_and_writes_total_area(tmp_path, monkeypatch, capsys):
    # generate_results writes "area.txt" relative to the working directory.
    monkeypatch.chdir(tmp_path)

    generate_results([Square(side=2), Rectangle(width=2, height=3)])

    # The areas of every shape are summed into a single total (4.0 + 6.0).
    expected = "Total area: 10.0\n"

    # Printed to the console...
    assert capsys.readouterr().out == expected
    # ...and written to area.txt.
    assert (tmp_path / "area.txt").read_text() == expected


def test_empty_list_produces_zero_total(tmp_path, monkeypatch, capsys):
    monkeypatch.chdir(tmp_path)

    generate_results([])

    expected = "Total area: 0.0\n"

    assert capsys.readouterr().out == expected
    assert (tmp_path / "area.txt").read_text() == expected
