FROM ubuntu:20.04 as build
RUN apt-get update && apt-get install -y build-essential
WORKDIR /app
COPY src/hello.c /app/
RUN gcc -o hello hello.c


FROM ubuntu:20.04 as final
WORKDIR /app
COPY --from=build /app/hello ./
ENTRYPOINT [ "/app/hello" ]