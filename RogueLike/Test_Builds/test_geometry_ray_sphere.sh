touch ./Tests/Geometry/Test_Ray_Sphere.cs \
    && msbuild -p:StartupObject=Rogue_Like.Test_Ray_Sphere \
    && chmod +x ./bin/Debug/rogue_like.exe \
    && ./bin/Debug/rogue_like.exe $1 $2 $3 $4 $5 $6 $7
