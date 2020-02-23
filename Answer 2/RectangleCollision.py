# Rect constructor
class Rect:
    def __init__(self, name, cx, cy, w, h):
        self.name = name
        self.cx = cx
        self.cy = cy
        self.w = w
        self.halfW = w/2.0
        self.h = h
        self.halfH = h/2.0

# Rect definitions
rectA = Rect("A", 2, 3, 5, 6)
rectB = Rect("B", 5, 1, 10, 6)
rectC = Rect("C", 1, 5.9, 4, 2)

# Check rect bounds between 2 rectangles
def CheckBounds(a,b):
    x = False
    y = False
    if ((a.cx > b.cx and (a.cx - a.halfW <= b.cx + b.halfW)) or
        (a.cx <= b.cx and (a.cx + a.halfW >= b.cx - b.halfW))):
        x = True
    if ((a.cy > b.cy and (a.cy - a.halfH <= b.cy + b.halfH)) or
        (a.cy <= b.cy and (a.cy + a.halfH >= b.cy - b.halfH))):
        y = True
    return (x and y)

# Collision function between 3 rectangles
def CheckCollisions(a, b, c):
    # Collision between a and b
    if (CheckBounds(a, b)):
        print("%s collides with %s" %(a.name, b.name))
    # Collision between a and c
    if (CheckBounds(a, c)):
        print("%s collides with %s" %(a.name, c.name))
    # Collision between b and c
    if (CheckBounds(b, c)):
        print("%s collides with %s" %(b.name, c.name))

# Function call
CheckCollisions(rectA, rectB, rectC)
