#include <iostream>
#include <math.h>

bool xyz(int n)
{
    float i = sqrt(n);
    int j = ceil(i);
    int k = 2;
    int x = k;
    while (x <= j)
    {
        if (!(n % x))
            return false;
        else
            x++;
    }
    return true;
}

int main()
{
    std::cout << "true is: " << true << "\n";
    std::cout << "false is: " << false << "\n";
    for (size_t i = 0; i < 51; i++)
    {
        if (xyz(i))
            std::cout << i << "\n";
    }
    return 0;
}