"""Generates banner.svg and features.svg for README.

Usage: python generate.py <output folder> [accent] [accent deep]
Example (lavender): python .github/readme/generate.py .github/readme "#b9a8ff" "#7a68d6"
"""

import random
import sys
from pathlib import Path

OUT = Path(sys.argv[1])
OUT.mkdir(parents=True, exist_ok=True)

# Palette. Change ACCENT / ACCENT_DEEP to switch the whole look (e.g. #b9a8ff / #7a68d6 for lavender).
BG = "#09090b"
PANEL = "#0e0e11"
PANEL_2 = "#131317"
LINE = "#2a2a31"
LINE_HI = "#3a3a44"
TEXT = "#ececf1"
MUTED = "#85858f"
DIM = "#55555e"
ACCENT = sys.argv[2] if len(sys.argv) > 2 else "#ff5468"
ACCENT_DEEP = sys.argv[3] if len(sys.argv) > 3 else "#a3303d"
TICK_OFF = "#24242a"
DOT_OFF = "rgba(255,255,255,.055)"

SANS = "'Segoe UI', Inter, 'Helvetica Neue', Arial, sans-serif"
MONO = "ui-monospace, 'SF Mono', 'Cascadia Mono', Consolas, Menlo, monospace"

FONT = {
    "0": ["01110", "10001", "10011", "10101", "11001", "10001", "01110"],
    "1": ["00100", "01100", "00100", "00100", "00100", "00100", "01110"],
    "2": ["01110", "10001", "00001", "00010", "00100", "01000", "11111"],
    "3": ["11111", "00010", "00100", "00010", "00001", "10001", "01110"],
    "4": ["00010", "00110", "01010", "10010", "11111", "00010", "00010"],
    "5": ["11111", "10000", "11110", "00001", "00001", "10001", "01110"],
    "6": ["00110", "01000", "10000", "11110", "10001", "10001", "01110"],
    "7": ["11111", "00001", "00010", "00100", "01000", "01000", "01000"],
    "8": ["01110", "10001", "10001", "01110", "10001", "10001", "01110"],
    "9": ["01110", "10001", "10001", "01111", "00001", "00010", "01100"],
    ".": ["00000", "00000", "00000", "00000", "00000", "01100", "01100"],
    "-": ["00000", "00000", "00000", "11111", "00000", "00000", "00000"],
    "A": ["01110", "10001", "10001", "11111", "10001", "10001", "10001"],
    "C": ["01110", "10001", "10000", "10000", "10000", "10001", "01110"],
    "D": ["11100", "10010", "10001", "10001", "10001", "10010", "11100"],
    "E": ["11111", "10000", "10000", "11110", "10000", "10000", "11111"],
    "K": ["10001", "10010", "10100", "11000", "10100", "10010", "10001"],
    "M": ["10001", "11011", "10101", "10101", "10001", "10001", "10001"],
    "R": ["11110", "10001", "10001", "11110", "10100", "10010", "10001"],
    "S": ["01111", "10000", "10000", "01110", "00001", "00001", "11110"],
    "T": ["11111", "00100", "00100", "00100", "00100", "00100", "00100"],
    "U": ["10001", "10001", "10001", "10001", "10001", "10001", "01110"],
}


# App icon emblem (Resources/Icon.ico) as a 44x49 grid: '#' bright, '+' mid, '.' off.
EMBLEM = [
    "...+###..............................###+...",
    "..#####+............+##+............+#####..",
    ".++..+##...........+####+...........##+..++.",
    ".+....+##..........+#####..........+#+....+.",
    ".......+#+.........######.........+#+.......",
    "........+#+........######........+#+........",
    ".........+#+.......######.......+#+.........",
    "..........###.......####+......###..........",
    ".##.......###...##+.####..#+...###.......##.",
    ".######.+##+...+###..##..###+...+##+.######.",
    ".#..+##.##..+########+++#######...##.##+..#.",
    ".#......#..######################..#......#.",
    "+#......#+.######################.+#......#+",
    "##.....##..######################..##.....+#",
    ".#+...+#+.########################.+#+...+#.",
    "......+#.+###..##############..###+.#+...+..",
    ".......#.######+#############+#####.#.......",
    ".........######..##########+.######.........",
    ".........######+###########++######.........",
    ".........#####..############..#####.........",
    ".........##+##..############..##+##.........",
    ".........###....############....+##.........",
    ".........####...############...####.........",
    "......+.+####...+##########+...####+........",
    ".......+#####...+##########+...#####........",
    ".........####...############...####+........",
    ".........####...############...####.........",
    ".........+###..+#############..+##+.........",
    "..........###..##############..###..........",
    "..........###+.##############+.###..........",
    "........#.####.+#############.####.#........",
    ".......##.###.#.############+#.###.##.......",
    "......+##.####+.############.+.###.##+......",
    "......##...++...############..+##...##......",
    ".....##........+############.+.......##.....",
    "....##+......#.+###++###+#+#.#.+.....+##....",
    "...#+.......+#.+++#+.+##.#.#.+.#.......+#...",
    "..+###......##.++.++.+##.#.#.+.##.....###+..",
    "...+###...+##+....+..++#.#.#.+.+##...###+...",
    ".....+##..##......+..+++.+.#....##+.###.....",
    "......+##.............+....+.......###......",
    ".......+##.......+........+.......###.......",
    "......+.+##.....#..........#.....##+.+......",
    "......##.###..+#+..........+#+..###.##......",
    ".....###+.#+###+............+###+#..###.....",
    "......+##++###................####.##++.....",
    "..........+#+.+..............+.+##..........",
    "..........#..+##............###..#..........",
    "................+..........+................",
]


def glyph_width(d, g):
    return 5 * d + 4 * g


def text_width(s, d, g, spacing):
    return len(s) * glyph_width(d, g) + (len(s) - 1) * spacing


def dots(s, x, y, d, g, spacing, on, off=DOT_OFF):
    """Dot-matrix string. Lit dots in `on`, unlit ones barely visible."""
    off_rects, on_rects = [], []
    cx = x
    for ch in s:
        for r, row in enumerate(FONT[ch]):
            for c, bit in enumerate(row):
                px, py = cx + c * (d + g), y + r * (d + g)
                (on_rects if bit == "1" else off_rects).append(f'<rect x="{px}" y="{py}" width="{d}" height="{d}"/>')
        cx += glyph_width(d, g) + spacing
    return (f'<g fill="{off}">{"".join(off_rects)}</g>'
            f'<g fill="{on}">{"".join(on_rects)}</g>')


def notch(x, y, w, h, s):
    """Rectangle with stepped pixel corners."""
    p = [
        (0, 2 * s), (s, 2 * s), (s, s), (2 * s, s), (2 * s, 0),
        (w - 2 * s, 0), (w - 2 * s, s), (w - s, s), (w - s, 2 * s), (w, 2 * s),
        (w, h - 2 * s), (w - s, h - 2 * s), (w - s, h - s), (w - 2 * s, h - s), (w - 2 * s, h),
        (2 * s, h), (2 * s, h - s), (s, h - s), (s, h - 2 * s), (0, h - 2 * s),
    ]
    return " ".join(f"{x + a:g},{y + b:g}" for a, b in p)


def px_box(x, y, w, h, s, border, fill):
    return (f'<polygon points="{notch(x, y, w, h, s)}" fill="{border}"/>'
            f'<polygon points="{notch(x + 1, y + 1, w - 2, h - 2, s)}" fill="{fill}"/>')


def grain(w, h, opacity=0.13):
    return (f'<filter id="grain" x="0" y="0" width="100%" height="100%">'
            f'<feTurbulence type="fractalNoise" baseFrequency=".9" numOctaves="2" stitchTiles="stitch"/>'
            f'<feColorMatrix values="0 0 0 0 1  0 0 0 0 1  0 0 0 0 1  0 0 0 .9 0"/></filter>'
            f'<rect width="{w}" height="{h}" filter="url(#grain)" opacity="{opacity}" style="mix-blend-mode:overlay"/>')


def dust(w, h, n, seed, avoid=()):
    rnd = random.Random(seed)
    out = []
    while len(out) < n:
        x, y = rnd.randrange(12, w - 12), rnd.randrange(12, h - 12)
        if any(ax <= x <= ax + aw and ay <= y <= ay + ah for ax, ay, aw, ah in avoid):
            continue
        color = ACCENT_DEEP if rnd.random() < 0.33 else "#2c2c33"
        out.append(f'<rect x="{x}" y="{y}" width="2" height="2" fill="{color}"/>')
    return "".join(out)


def marker(x, y, size=8, color=None):
    return f'<rect x="{x}" y="{y}" width="{size}" height="{size}" fill="{color or ACCENT}"/>'


def caps(x, y, s, size=12, color=MUTED, spacing=0.22, anchor="start", weight=600):
    return (f'<text x="{x}" y="{y}" font-family="{MONO}" font-size="{size}" font-weight="{weight}" '
            f'letter-spacing="{size * spacing:.2f}" fill="{color}" text-anchor="{anchor}">{s}</text>')


def chip(x, y, label, active=False):
    size, ls = 12, 1.6
    w = round(len(label) * (size * 0.6 + ls) + 30)
    h = 30
    border, fill, color = (ACCENT_DEEP, PANEL_2, ACCENT) if active else (LINE_HI, PANEL_2, MUTED)
    return w, (px_box(x, y, w, h, 2, border, fill) +
               f'<text x="{x + 15}" y="{y + 19.5}" font-family="{MONO}" font-size="{size}" font-weight="600" '
               f'letter-spacing="{ls}" fill="{color}">{label}</text>')


def ring(cx, cy, scale, lit, total=44):
    ticks = []
    for k in range(total):
        color = ACCENT if k < lit else TICK_OFF
        ticks.append(f'<rect x="48.5" y="3" width="3" height="6" fill="{color}" transform="rotate({360 / total * k:.3f} 50 50)"/>')
    # One bright tick runs around the ring.
    sweep = (f'<rect x="48.5" y="3" width="3" height="6" fill="{TEXT}">'
             f'<animateTransform attributeName="transform" type="rotate" values="0 50 50;360 50 50" dur="6s" repeatCount="indefinite"/></rect>')
    return (f'<g transform="translate({cx - 50 * scale} {cy - 50 * scale}) scale({scale})">'
            f'{"".join(ticks)}{sweep}</g>')


def emblem(cx, cy, step, radius):
    """Icon emblem as a mosaic of squares on a faint round dot field."""
    rnd = random.Random(3)
    d = step * 0.75
    cols, rows = len(EMBLEM[0]), len(EMBLEM)
    x0, y0 = cx - cols * step / 2, cy - rows * step / 2
    off, mid, lit = [], [], []
    n = int(radius // step) + 2
    for r in range(-n, rows + n):
        for c in range(-n, cols + n):
            x, y = x0 + c * step, y0 + r * step
            v = EMBLEM[r][c] if 0 <= r < rows and 0 <= c < cols else "."
            if v == "#":
                lit.append(f'<rect x="{x:.1f}" y="{y:.1f}" width="{d:.1f}" height="{d:.1f}" opacity="{rnd.uniform(.78, 1):.2f}"/>')
            elif v == "+":
                mid.append(f'<rect x="{x:.1f}" y="{y:.1f}" width="{d:.1f}" height="{d:.1f}"/>')
            elif (x + d / 2 - cx) ** 2 + (y + d / 2 - cy) ** 2 < radius ** 2:
                off.append(f'<rect x="{x:.1f}" y="{y:.1f}" width="{d:.1f}" height="{d:.1f}"/>')
    return (f'<g fill="rgba(255,255,255,.025)">{"".join(off)}</g><g fill="{MUTED}" opacity=".5">{"".join(mid)}</g>'
            f'<g fill="{TEXT}">{"".join(lit)}</g>')


def banner():
    W, H = 1280, 470
    parts = [f'<rect width="{W}" height="{H}" fill="{BG}"/>', px_box(0, 0, W, H, 5, LINE, PANEL)]

    # Top bar.
    parts.append(marker(48, 44))
    parts.append(caps(68, 52.5, "CRANCH-FUR / CURSED-MARKET"))
    parts.append(caps(W - 48, 52.5, "DEAD BY DAYLIGHT  PROXY TOOLKIT", anchor="end", color=DIM))
    parts.append(f'<rect x="48" y="76" width="{W - 96}" height="1" fill="{LINE}"/>')

    # Title.
    d, g, sp = 10, 3, 26
    title_off = "rgba(255,255,255,.035)"
    tx, ty = 48, 112
    parts.append(dots("CURSED", tx, ty, d, g, sp, TEXT, title_off))
    line2_y = ty + 7 * (d + g) + 22
    parts.append(dots("MARKET", tx, line2_y, d, g, sp, ACCENT, title_off))
    cursor_x = tx + text_width("MARKET", d, g, sp) + sp
    parts.append(f'<rect x="{cursor_x}" y="{line2_y + 6 * (d + g)}" width="{glyph_width(d, g)}" height="{d}" fill="{ACCENT}">'
                 f'<animate attributeName="opacity" values="1;1;0;0" keyTimes="0;.5;.5;1" dur="1.1s" repeatCount="indefinite"/></rect>')

    # Subtitle.
    sub_y = line2_y + 7 * (d + g) + 46
    parts.append(f'<text x="{tx}" y="{sub_y}" font-family="{SANS}" font-size="19" fill="{MUTED}">'
                 f'Local proxy that gives temporary access to characters, cosmetics, perks, items and add-ons.</text>')

    # Chips.
    cx = tx
    for i, label in enumerate(["V9.1", "WINDOWS 10 / 11", "C# / .NET 4.8.1", "FIDDLERCORE", "GPL-3.0", "16 LANGUAGES"]):
        w, svg = chip(cx, sub_y + 26, label, active=(i == 0))
        parts.append(svg)
        cx += w + 8

    # Ring with icon emblem.
    rcx, rcy = 1040, 236
    parts.append(ring(rcx, rcy, 2.7, 33))
    parts.append(emblem(rcx, rcy, 3.2, 98))
    parts.append(caps(rcx, rcy + 158, "VERSION 9.1", size=11, color=MUTED, spacing=0.3, anchor="middle"))

    parts.append(dust(W, H, 28, 7, avoid=[(0, 30, 1280, 52), (40, 100, 680, 260), (900, 100, 280, 320), (40, 330, 820, 100)]))
    parts.append(grain(W, H))

    return (f'<svg xmlns="http://www.w3.org/2000/svg" width="{W}" height="{H}" viewBox="0 0 {W} {H}" role="img" '
            f'aria-label="Cursed Market, version 9.1">{"".join(parts)}</svg>')


FEATURES = [
    ("GAME CHANGERS", ["Customizations King and KillSwitch", "Remover bring back hidden items."]),
    ("QUEUE OVERLAY", ["Queue position, lobby and match", "start on top of the game, with sound."]),
    ("CHARACTERS PRESET", ["Custom prestige and bloodweb", "level for every character."]),
    ("EXTRA LOGIC", ["Your own C# script modules", "hooked into game traffic."]),
    ("SIMPLE TIMER", ["Middle mouse button starts a", "60 second countdown in match."]),
    ("ADAPTIVE CROSSHAIR", ["Shown only while the right", "mouse button is held down."]),
    ("THEMES", ["Six built-in looks, from", "Dark Memories to Dracula."]),
    ("16 LANGUAGES", ["Follows Windows language or", "the -lang= launch parameter."]),
]


def features():
    W, cols, gap, pad = 1280, 4, 10, 32
    cw = (W - 2 * pad - (cols - 1) * gap) / cols
    ch = 166
    rows = (len(FEATURES) + cols - 1) // cols
    top = 72
    H = top + rows * ch + (rows - 1) * gap + pad
    parts = [f'<rect width="{W}" height="{H}" fill="{BG}"/>', px_box(0, 0, W, H, 5, LINE, PANEL)]
    parts.append(marker(pad, 30, 6))
    parts.append(caps(pad + 16, 37, "WHAT'S INSIDE"))
    parts.append(caps(W - pad, 37, f"{len(FEATURES):02d} MODULES", anchor="end", color=DIM))

    for i, (title, desc) in enumerate(FEATURES):
        r, c = divmod(i, cols)
        x, y = pad + c * (cw + gap), top + r * (ch + gap)
        parts.append(px_box(x, y, cw, ch, 3, LINE, PANEL_2))
        parts.append(dots(f"{i + 1:02d}", x + 18, y + 18, 4, 1, 5, ACCENT))
        parts.append(f'<rect x="{x + cw - 24}" y="{y + 18}" width="6" height="6" fill="{DIM}"/>')
        parts.append(f'<text x="{x + 18}" y="{y + 84}" font-family="{SANS}" font-size="17" font-weight="700" '
                     f'letter-spacing=".6" fill="{TEXT}">{title}</text>')
        for j, line in enumerate(desc):
            parts.append(f'<text x="{x + 18}" y="{y + 112 + j * 21}" font-family="{SANS}" font-size="15" fill="{MUTED}">{line}</text>')

    parts.append(dust(W, H, 10, 11, avoid=[(0, 20, W, 26), (pad, top, W - 2 * pad, H - top - pad)]))
    parts.append(grain(W, H, 0.11))
    return (f'<svg xmlns="http://www.w3.org/2000/svg" width="{W}" height="{H:g}" viewBox="0 0 {W} {H:g}" role="img" '
            f'aria-label="Features overview">{"".join(parts)}</svg>')


(OUT / "banner.svg").write_text(banner(), encoding="utf-8")
(OUT / "features.svg").write_text(features(), encoding="utf-8")
print("ok")
