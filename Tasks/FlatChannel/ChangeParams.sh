#!/bin/bash

cd "$(dirname "$0")"

echo "Рабочая директория: $(pwd)"

PARAMS_FILE="params.txt"
BLOCKMESH="system/blockMeshDict"
U_FILE="0/U"

# Проверка файлов
if [ ! -f "$PARAMS_FILE" ]; then
    echo "Ошибка: $PARAMS_FILE не найден"
    exit 1
fi

if [ ! -f "$BLOCKMESH" ]; then
    echo "Ошибка: $BLOCKMESH не найден"
    exit 1
fi

# -----------------------------
# trim пробелов
# -----------------------------
trim() {
    echo "$1" | xargs
}

# -----------------------------
# Обновление переменной в blockMeshDict
# Поддерживает:
#   25;
#   -25;
#   25.5;
#   -25.5;
# -----------------------------
update_blockmesh_var() {

    local var_name="$1"
    local new_value="$2"

    sed -i -E \
    "s/^(${var_name}[[:space:]]+)-?[0-9]*\.?[0-9]+;/\1${new_value};/" \
    "$BLOCKMESH"
}

# -----------------------------
# Обновление U
# -----------------------------
update_U() {

    local new_value="$1"

    if [ -f "$U_FILE" ]; then

        sed -i -E \
        "s/^(U[[:space:]]+)-?[0-9]*\.?[0-9]+;/\1${new_value};/" \
        "$U_FILE"

        echo "Обновлено U = $new_value в $U_FILE"

    else

        echo "Предупреждение: $U_FILE не найден"

    fi
}

# -----------------------------
# Чтение params.txt
# Формат:
#
# name
# value
# -----------------------------
mapfile -t lines < "$PARAMS_FILE"

for (( i=0; i<${#lines[@]}; i+=2 ))
do

    var_name="$(trim "${lines[i]}")"
    var_value="$(trim "${lines[i+1]}")"

    # Пропуск пустых строк
    if [ -z "$var_name" ]; then
        continue
    fi

    if [[ "$var_name" == "U" ]]; then

        update_U "$var_value"

    else

        update_blockmesh_var "$var_name" "$var_value"

        echo "Обновлено $var_name = $var_value"

    fi

done

echo "Готово."
