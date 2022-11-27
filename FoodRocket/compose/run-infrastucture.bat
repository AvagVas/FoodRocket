docker compose -f .\infrastructure.yml up -d
timeout 10
docker exec mongo1 /scripts/rs-init.sh
