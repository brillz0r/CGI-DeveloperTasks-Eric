"""Pytest configuration for the test suite.

The app is run as scripts out of ``src`` (so ``src`` is the import root)
rather than installed as a package. Adding ``src`` to ``sys.path`` here lets
the tests import application modules the same way the app does, e.g.
``from shapes.circle import Circle``.
"""

import os
import sys

SRC_DIR = os.path.abspath(os.path.join(os.path.dirname(__file__), "..", "src"))

if SRC_DIR not in sys.path:
    sys.path.insert(0, SRC_DIR)
