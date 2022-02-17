# Importing modules
import math
import numpy as np
from random import uniform

# Function to show n points of a semicircle
def PrintPoints(cx, cy, r, n):
    for i in np.arange(0, n):
        # Select random angle
        ang = uniform(0, 180)
        # Calculate x and y components
        x = r * math.cos(math.radians(ang)) + cx
        y = r * math.sin(math.radians(ang)) + cy
        print("(%f, %f)" %(x,y))

# Function call
PrintPoints(0, 0, 1, 5)
