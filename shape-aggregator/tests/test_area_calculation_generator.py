from area_calculation_generator import generate_results
from shapes.square import Square
from shapes.rectangle import Rectangle


def test_prints_and_writes_areas(tmp_path, monkeypatch, capsys):
    # generate_results writes "area.txt" relative to the working directory.
    monkeypatch.chdir(tmp_path)

    generate_results([Square(side=2), Rectangle(width=2, height=3)])

    expected = "Total area: 4.0\nTotal area: 6.0\n"

    # Printed to the console...
    assert capsys.readouterr().out == expected
    # ...and written to area.txt.
    assert (tmp_path / "area.txt").read_text() == expected


def test_empty_list_produces_empty_output(tmp_path, monkeypatch, capsys):
    monkeypatch.chdir(tmp_path)

    generate_results([])

    assert capsys.readouterr().out == ""
    assert (tmp_path / "area.txt").read_text() == ""
