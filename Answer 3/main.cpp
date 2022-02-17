#include <iostream>
#include <math.h>
#include <random>

#define PI 3.14159265359

struct Vector2
{
    float X;
    float Y;

    Vector2(float _x, float _y)
    : X(_x), Y(_y)
    {}
};

void PrintPoints(Vector2 center, float radius, int quantity)
{
    std::default_random_engine engine;
    std::uniform_real_distribution<float> dist(0.0f, 1.0f);

    for (size_t i = 0; i < quantity; i++)
    {
        float angle = dist(engine);
        float radians = angle * PI;
        float x = radius * cosf(radians) + center.X;
        float y = radius * sinf(radians) + center.Y;
        std::cout << "(" << x << ", " << y << ") at angle " << angle << "\n";
    }
}

int main()
{
    PrintPoints(Vector2(0.0f, 0.0f), 1.0f, 5);
    system("pause");
    return 0;
}