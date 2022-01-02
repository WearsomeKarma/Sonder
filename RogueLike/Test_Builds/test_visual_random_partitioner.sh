touch ./Tests/Debug/Tests/Test_Visual_Random_Partitioner.cs \
    && msbuild -p:StartupObject=Rogue_Like.Test_Visual_Random_Partitioner \
    && chmod +x ./bin/Debug/rogue_like.exe \
    && ./bin/Debug/rogue_like.exe $1 $2 $3 $4
