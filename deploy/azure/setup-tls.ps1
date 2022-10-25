param(
    [string]$containerRegistry
)

CERT_MANAGER_REGISTRY=quay.io
CERT_MANAGER_TAG=v1.8.0
CERT_MANAGER_IMAGE_CONTROLLER=jetstack/cert-manager-controller
CERT_MANAGER_IMAGE_WEBHOOK=jetstack/cert-manager-webhook
CERT_MANAGER_IMAGE_CAINJECTOR=jetstack/cert-manager-cainjector

az acr import --name $containerRegistry --source $CERT_MANAGER_REGISTRY/${CERT_MANAGER_IMAGE_CONTROLLER}:${CERT_MANAGER_TAG} --image ${CERT_MANAGER_IMAGE_CONTROLLER}:${CERT_MANAGER_TAG}
az acr import --name $containerRegistry --source $CERT_MANAGER_REGISTRY/${CERT_MANAGER_IMAGE_WEBHOOK}:${CERT_MANAGER_TAG} --image ${CERT_MANAGER_IMAGE_WEBHOOK}:${CERT_MANAGER_TAG}
az acr import --name $containerRegistry --source $CERT_MANAGER_REGISTRY/${CERT_MANAGER_IMAGE_CAINJECTOR}:${CERT_MANAGER_TAG} --image ${CERT_MANAGER_IMAGE_CAINJECTOR}:${CERT_MANAGER_TAG}