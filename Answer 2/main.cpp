#include <iostream>
#include <math.h>

struct Vector2
{
    float X;
    float Y;

    Vector2()
    {}

    Vector2(float _x, float _y)
    : X(_x), Y(_y)
    {}

    bool operator ==(Vector2 other) const
    {
        return X == other.X && Y == other.Y;
    }
};

class Rectangle
{
private:
    std::string name;
    Vector2 center;
    float width;
    float height;

    float halfWidth;
    float halfHeight;
    Vector2 minBounds;
    Vector2 maxBounds;

    bool CollisionByCenter(Rectangle other) const
    {
        return center == other.center;
    }

    bool IsAtLeftFrom(Rectangle other) const
    {
        return center.X < other.center.X;
    }

    bool IsAtRightFrom(Rectangle other) const
    {
        return center.X > other.center.X;
    }

    bool IsAboveFrom(Rectangle other) const
    {
        return center.Y > other.center.Y;
    }

    bool IsBelowFrom(Rectangle other) const
    {
        return center.Y < other.center.Y;
    }

public:
    Rectangle(std::string _name, Vector2 _center, float _width, float _height)
    : name(_name), center(_center), width(_width), height(_height)
    {
        halfWidth = width / 2.0f;
        halfHeight = height / 2.0f;
        minBounds.X = center.X - halfWidth;
        minBounds.Y = center.Y - halfWidth;
        maxBounds.X = center.X + halfWidth;
        maxBounds.Y = center.Y + halfWidth;
    }

    bool IsCollidingWith(Rectangle other) const
    {
        if (CollisionByCenter(other))
        {
            std::cout << "rectangle " << name << " collides with " << other.name  << " by center\n";
            return true;
        }
        
        bool collideInX = (IsAtRightFrom(other) && minBounds.X <= other.maxBounds.X)
                        || (IsAtLeftFrom(other) && maxBounds.X >= other.minBounds.X);

        bool collideInY = (IsAboveFrom(other) && minBounds.Y <= other.maxBounds.Y)
                        || (IsBelowFrom(other) && maxBounds.Y >= other.minBounds.Y);

        if (collideInX && collideInY)
        {
            std::cout << "rectangle " << name << " collides with " << other.name << "\n";
            return true;
        }

        std::cout << "rectangle " << name << " does not collide with " << other.name << "\n";
        return false;
    }
};

int main()
{
    Rectangle rectA("A", Vector2(1.3f, 2.6f), 3.0f, 1.31f);
    Rectangle rectB("B", Vector2(2.3f, 1.6f), 4.3f, 2.31f);
    Rectangle rectC("C", Vector2(3.1f, 6.2f), 1.0f, 4.12f);

    rectA.IsCollidingWith(rectB);
    rectA.IsCollidingWith(rectC);
    rectB.IsCollidingWith(rectC);

    system("pause");

    return 0;
}