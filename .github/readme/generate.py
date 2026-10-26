"""Generates banner.svg and features.svg for README.

Usage: python generate.py <output folder> [accent] [accent deep]
Example (red): python .github/readme/generate.py .github/readme "#ff5468" "#a3303d"
"""

import random
import sys
from pathlib import Path

OUT = Path(sys.argv[1])
OUT.mkdir(parents=True, exist_ok=True)

# Palette. ACCENT / ACCENT_DEEP can be overridden from the command line.
BG = "#09090b"
PANEL = "#0e0e11"
PANEL_2 = "#131317"
LINE = "#2a2a31"
LINE_HI = "#3a3a44"
TEXT = "#ececf1"
MUTED = "#85858f"
DIM = "#55555e"
ACCENT = sys.argv[2] if len(sys.argv) > 2 else "#b9a8ff"
ACCENT_DEEP = sys.argv[3] if len(sys.argv) > 3 else "#7a68d6"
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
}


# Title in Manrope ExtraBold (112 px), converted to outlines so it looks the same without the font installed.
# Word: (width, cap height, path). Top of capitals is at y = 0.
TITLE = {
    "CURSED": (455.7, 80.6, "M42.4 82.3Q30.4 82.3 21.6 77.1Q12.8 71.8 8.1 62.3Q3.4 52.9 3.4 40.3Q3.4 27.8 8.1 18.3Q12.8 8.8 21.6 3.6Q30.4 -1.7 42.4 -1.7Q56.3 -1.7 65.8 5.2Q75.2 12.1 79.1 23.9L63.7 28.1Q61.5 20.8 56.2 16.7Q50.8 12.7 42.4 12.7Q34.8 12.7 29.7 16.1Q24.5 19.5 22 25.7Q19.4 31.9 19.4 40.3Q19.4 48.7 22 54.9Q24.5 61.2 29.7 64.6Q34.8 68 42.4 68Q50.8 68 56.2 63.9Q61.5 59.8 63.7 52.5L79.1 56.8Q75.2 68.5 65.8 75.4Q56.3 82.3 42.4 82.3ZM123.2 82.3Q113.5 82.3 106.1 78.4Q98.7 74.4 94.5 67.3Q90.4 60.1 90.4 50.4V0.1L105.8 0V50.3Q105.8 54.3 107.2 57.5Q108.5 60.8 110.9 63.1Q113.3 65.5 116.5 66.7Q119.6 68 123.2 68Q126.9 68 130.1 66.7Q133.2 65.4 135.6 63.1Q137.9 60.7 139.3 57.5Q140.6 54.2 140.6 50.3V0H156V50.4Q156 60.1 151.9 67.3Q147.7 74.4 140.3 78.4Q133 82.3 123.2 82.3ZM170.7 80.6V0H204.8Q205.9 0 207.9 0.1Q209.9 0.2 211.5 0.4Q218.7 1.6 223.3 5.2Q227.9 8.8 230.1 14.4Q232.3 19.9 232.3 26.7Q232.3 36.8 227.3 44Q222.2 51.2 211.5 52.9L204.8 53.3H185.9V80.6ZM216.2 80.6 200.3 47.8 216 44.8 233.4 80.6ZM185.9 39.1H204.1Q205.3 39.1 206.7 39Q208.1 38.9 209.2 38.5Q212.3 37.7 214 35.7Q215.7 33.7 216.3 31.3Q217 28.8 217 26.7Q217 24.5 216.3 22Q215.7 19.6 214 17.6Q212.3 15.6 209.2 14.8Q208.1 14.4 206.7 14.3Q205.3 14.2 204.1 14.2H185.9ZM275.2 82.3Q266.2 82.3 259.1 79.2Q251.9 76 247.2 70.1Q242.6 64.2 241.4 56L257.3 53.6Q259 60.6 264.2 64.3Q269.5 68.1 276.2 68.1Q280 68.1 283.5 66.9Q287 65.7 289.3 63.4Q291.6 61.2 291.6 57.8Q291.6 56.6 291.2 55.4Q290.8 54.3 290 53.3Q289.2 52.2 287.6 51.4Q286.1 50.5 283.7 49.7L262.8 43.6Q260.4 42.9 257.3 41.7Q254.2 40.4 251.3 38.1Q248.4 35.8 246.5 32.1Q244.5 28.3 244.5 22.5Q244.5 14.4 248.6 9Q252.7 3.6 259.5 1Q266.4 -1.6 274.6 -1.6Q283 -1.5 289.5 1.3Q296.1 4 300.5 9.3Q304.9 14.5 306.9 22.1L290.4 24.9Q289.5 20.9 287.1 18.3Q284.7 15.6 281.4 14.2Q278.1 12.8 274.4 12.7Q270.8 12.5 267.7 13.6Q264.5 14.7 262.5 16.8Q260.5 18.9 260.5 21.7Q260.5 24.4 262.2 26Q263.8 27.7 266.2 28.7Q268.7 29.7 271.3 30.4L285.3 34.2Q288.4 35 292.2 36.4Q296 37.7 299.5 40.2Q303 42.6 305.3 46.6Q307.6 50.6 307.6 56.7Q307.6 63.2 304.9 68Q302.1 72.9 297.6 76Q293 79.2 287.2 80.8Q281.4 82.3 275.2 82.3ZM318.9 80.6V0H371.5V14.2H334.1V31.5H364.8V45.7H334.1V66.4H371.5V80.6ZM384 80.6V0H410.1Q411 0 414 0.1Q417 0.1 419.7 0.4Q429.4 1.6 436.2 7.2Q443 12.8 446.5 21.4Q450.1 30.1 450.1 40.3Q450.1 50.6 446.5 59.2Q443 67.8 436.2 73.4Q429.4 79 419.7 80.2Q417 80.5 414 80.6Q411 80.6 410.1 80.6ZM399.4 66.3H410.1Q411.6 66.3 414.2 66.2Q416.7 66.1 418.8 65.7Q424.1 64.7 427.4 60.8Q430.7 57 432.3 51.5Q433.9 46.1 433.9 40.3Q433.9 34.3 432.3 28.8Q430.6 23.4 427.3 19.7Q423.9 15.9 418.8 14.9Q416.7 14.4 414.2 14.4Q411.6 14.3 410.1 14.3H399.4Z"),
    "MARKET": (451.7, 80.6, "M7.8 80.6V0H21.5L48.2 53.5L74.8 0H88.5V80.6H74.3V32.5L50.8 80.6H45.5L22.1 32.5V80.6ZM97.6 80.6 122.2 0H144.9L169.6 80.6H153.9L131.9 9.5H134.9L113.2 80.6ZM112.3 63.8V49.6H154.9V63.8ZM178.7 80.6V0H212.7Q213.9 0 215.9 0.1Q217.9 0.2 219.4 0.4Q226.6 1.6 231.3 5.2Q235.9 8.8 238.1 14.4Q240.3 19.9 240.3 26.7Q240.3 36.8 235.2 44Q230.2 51.2 219.4 52.9L212.7 53.3H193.9V80.6ZM224.1 80.6 208.2 47.8 223.9 44.8 241.4 80.6ZM193.9 39.1H212Q213.2 39.1 214.6 39Q216 38.9 217.2 38.5Q220.3 37.7 221.9 35.7Q223.6 33.7 224.3 31.3Q224.9 28.8 224.9 26.7Q224.9 24.5 224.3 22Q223.6 19.6 221.9 17.6Q220.3 15.6 217.2 14.8Q216 14.4 214.6 14.3Q213.2 14.2 212 14.2H193.9ZM252.7 80.6V0H267.9V37.2L298.6 0H317.2L283.5 39.8L319.3 80.6H300.1L267.9 43.9V80.6ZM326.2 80.6V0H378.8V14.2H341.4V31.5H372.1V45.7H341.4V66.4H378.8V80.6ZM410 80.6V14.2H384.5V0H450.6V14.2H425.2V80.6Z"),
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
    W, H = 1280, 400
    parts = [f'<rect width="{W}" height="{H}" fill="{BG}"/>', px_box(0, 0, W, H, 5, LINE, PANEL)]

    # Top bar.
    parts.append(marker(48, 44))
    parts.append(caps(68, 52.5, "CRANCH-FUR / CURSED-MARKET"))
    parts.append(f'<rect x="48" y="76" width="{W - 96}" height="1" fill="{LINE}"/>')

    # Title.
    tx, ty = 48, 108
    w1, cap, path1 = TITLE["CURSED"]
    w2, _, path2 = TITLE["MARKET"]
    line2_y = ty + cap + 26
    parts.append(f'<path transform="translate({tx} {ty})" fill="{TEXT}" d="{path1}"/>')
    parts.append(f'<path transform="translate({tx} {line2_y})" fill="{ACCENT}" d="{path2}"/>')
    parts.append(f'<rect x="{tx + w2 + 16:.1f}" y="{line2_y + cap - 12:.1f}" width="40" height="12" fill="{ACCENT}">'
                 f'<animate attributeName="opacity" values="1;1;0;0" keyTimes="0;.5;.5;1" dur="1.1s" repeatCount="indefinite"/></rect>')

    # Chips.
    cx = tx
    for i, label in enumerate(["C# .NET Framework", "WINDOWS 10+", "GPL-3.0"]):
        w, svg = chip(cx, line2_y + cap + 36, label, active=(i == 0))
        parts.append(svg)
        cx += w + 8

    # Ring with icon emblem.
    rcx, rcy = 1040, 232
    parts.append(ring(rcx, rcy, 2.7, 33))
    parts.append(emblem(rcx, rcy, 3.2, 98))

    parts.append(dust(W, H, 28, 7, avoid=[(0, 30, 1280, 52), (40, 100, 560, 210), (900, 100, 280, 280), (40, 320, 560, 60)]))
    parts.append(grain(W, H))

    return (f'<svg xmlns="http://www.w3.org/2000/svg" width="{W}" height="{H}" viewBox="0 0 {W} {H}" role="img" '
            f'aria-label="Cursed Market">{"".join(parts)}</svg>')


FEATURES = [
    ("GAME CHANGERS", ["Customizations King and KillSwitch", "Remover bring back hidden items."]),
    ("QUEUE OVERLAY", ["Queue position, lobby and match", "start on top of the game, with sound."]),
    ("CHARACTERS PRESET", ["Custom prestige and bloodweb", "level for every character."]),
    ("EXTRA LOGIC", ["Your own C# script modules", "hooked into game traffic."]),
    ("SIMPLE TIMER", ["Middle mouse button starts a", "60 second countdown in match."]),
    ("ADAPTIVE CROSSHAIR", ["Shown only while the right", "mouse button is held down."]),
    ("THEMES", ["Several built-in looks,", "both dark and light."]),
    ("10+ LANGUAGES", ["Follows the Windows language", "or the -lang= launch parameter."]),
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
