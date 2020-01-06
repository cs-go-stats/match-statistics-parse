#./pack.sh match-statistics-parse 0.1.0

service_name=$1
version=$2

if [ "$#" -ne "2" ]; then
	echo "Service name and version aren't specified." 1>&2
	exit 64
fi

rm -rf ./out &&
	mkdir ./out &&
	cp -R ./match-statistics-parse ./out/match-statistics-parse &&

	rm -rf ./out/match-statistics-parse/bin/ && rm -rf ./out/match-statistics-parse/obj/ &&

	cd ./out/match-statistics-parse &&

	dotnet restore -r linux-musl-x64 -v m &&
	dotnet publish -c Release -o ./../pub -r linux-musl-x64 -v m &&

	cd ./../..

docker stop $service_name || true
docker container rm $service_name || true
docker images -q $service_name | xargs docker image rm

docker build -t $service_name:$version -f Dockerfile . && 
	docker create --name $service_name --network cs-go-stats_prime $service_name:$version &&
	docker start $service_name

rm -rf ./out