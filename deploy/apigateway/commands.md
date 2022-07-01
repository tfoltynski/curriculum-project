docker run --rm -it -v "$PWD\:`"C:\Users\2050182\source\repos\EAuction\curriculum-project\deploy\apigateway\`"" -p '9999:8001' -p '9990:8080' 'envoyproxy/envoy-dev' --mode validate -c 'C:\Users\2050182\source\repos\EAuction\curriculum-project\deploy\apigateway\envoy.yaml'

docker run --rm -v $(pwd)/envoy.yaml:/envoy.yaml envoyproxy/envoy-dev --mode validate -c envoy.yaml
