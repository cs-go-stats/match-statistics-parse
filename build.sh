#./build.sh contracts match-statistics-parse _

artifact_subfolder=$1
solution_name=$2
package_name=$3
nuget_key=b8e0f6c7-0f8d-3d80-83dc-eccb59ee6083

if [ "$#" -ne "3" ]; then
	echo "run script should be executed with <artifact_subfolder> <solution_name> <package_name> parameters." 1>&2
	exit 64
fi

docker-compose -p cs-go-stats up --no-recreate -d

rm -rf `cat folders_to_remove | sed 's/\\r//g'`

dotnet restore -v m ./$solution_name.sln && 
	dotnet build -v diag -c Release --no-incremental ./$solution_name.sln && 
	dotnet test -v n ./$solution_name.sln


echo ''
read -p 'Run finished. Pressing any key will terminate this script.'