### minikube

#### servives

- [Loadbalance](https://minikube.sigs.k8s.io/docs/handbook/accessing/)

  - open minikube tunnel (terminal will be hanging)

        $ minikube tunnel

  - exec service

        $ kubectl apply -f [svc].yaml

  - check service

        $ kubectl get svc
    