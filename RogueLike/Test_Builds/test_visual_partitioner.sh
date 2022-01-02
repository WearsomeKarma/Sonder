touch ./Tests/Debug/Test_Visual_Partitioner.cs && msbuild -p:StartupObject=Rogue_Like.Test_Visual_Partitioner && chmod +x ./bin/Debug/rogue_like.exe && ./bin/Debug/rogue_like.exe $1 $2 $3
