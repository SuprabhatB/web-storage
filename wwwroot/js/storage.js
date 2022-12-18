const storageType = {
    LOCAL: 0,
    SESSION: 1
}

export function setValueByKey(key, value, type) {
    if (key) {
        if (!value) {
            value = '';
        }

        if (!type) {
            type = storageType.LOCAL;
        }

        if (type == storageType.LOCAL) {
            localStorage.setItem(key, value);
        }
        else if (type == storageType.SESSION) {
            sessionStorage.setItem(key, value);
        }
    }
}

export function getValueByKey(key, type) {
    let value;

    if (key) {
        if (!type) {
            type = storageType.LOCAL;
        }

        if (type == storageType.LOCAL) {
            value = localStorage.getItem(key);
        }
        else if (type == storageType.SESSION) {
            value = sessionStorage.getItem(key);
        }
    }

    return value;
}

export function removeByKey(key, type) {
    if (key) {
        if (!type) {
            type = storageType.LOCAL;
        }

        if (type == storageType.LOCAL) {
            localStorage.removeItem(key);
        }
        else if (type == storageType.SESSION) {
            sessionStorage.removeItem(key);
        }
    }
}

export function removeAllKeys(type) {
    if (!type) {
        type = storageType.LOCAL;
    }

    if (type == storageType.LOCAL) {
        localStorage.clear();
    }
    else if (type == storageType.SESSION) {
        sessionStorage.clear();
    }
}