#/bin/bash
docker compose -f ./infrastructure.yml up -d
echo "docker stuff finished, wait appr 10 secs"
sleep 5000
echo "setup mongo replicas"
docker exec mongo1 /scripts/rs-init.sh
