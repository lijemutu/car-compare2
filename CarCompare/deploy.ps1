# PowerShell version of deploy.sh

# Build Docker image
docker build --no-cache -t erloto/carcompare:latest .

# Save Docker image to tar
docker save erloto/carcompare:latest -o carcompare.tar

# Copy tar file to remote server
scp carcompare.tar erlo@192.168.68.110:~/deployments/carcompare/

# Import image on remote server via SSH
ssh erlo@192.168.68.110  "sudo k3s ctr images import ~/deployments/carcompare/carcompare.tar"

# Apply Kubernetes deployment and service
kubectl apply -f deployment.yaml
kubectl apply -f service.yaml
# Force a rolling update to pick up the new image
kubectl rollout restart deployment/carcompare-deployment