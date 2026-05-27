#!/bin/bash
cd "$(dirname "$0")"
echo "$(dirname "$0")"
PARAMS_FILE="params.txt"
BLOCKMESH="system/blockMeshDict"
U_FILE="0/U"   # если у вас файл в 0_org/U — поменяйте здесь

# Проверка наличия файлов
if [ ! -f "$PARAMS_FILE" ]; then
    echo "Ошибка: $PARAMS_FILE не найден"
    exit 1
fi

# Обновление переменной в blockMeshDict (кроме U)
update_blockmesh_var() {
    local var_name="$1"
    local new_value="$2"
    sed -i -E "s/^(${var_name}[[:space:]]+)[0-9]+;/\1${new_value};/" "$BLOCKMESH"
}

# Обновление U в файле 0/U
update_U() {
    local new_value="$1"
    if [ -f "$U_FILE" ]; then
        sed -i -E "s/^(U[[:space:]]+)-?[0-9]+;/\1${new_value};/" "$U_FILE"
        echo "Обновлено U = $new_value в $U_FILE"
    else
        echo "Предупреждение: $U_FILE не найден, U не обновлён"
    fi
}

# Чтение params.txt (пары: имя_переменной, значение)
mapfile -t lines < "$PARAMS_FILE"

for (( i=0; i<${#lines[@]}; i+=2 )); do
    var_name="${lines[i]}"
    var_value="${lines[i+1]}"
    if [[ "$var_name" == "U" ]]; then
        update_U "$var_value"
    else
        if [ -f "$BLOCKMESH" ]; then
            update_blockmesh_var "$var_name" "$var_value"
            echo "Обновлено $var_name = $var_value в blockMeshDict"
        else
            echo "Предупреждение: $BLOCKMESH не найден"
        fi
    fi
done

echo "Готово."
