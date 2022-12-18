import { setValueByKey, getValueByKey, removeByKey, removeAllKeys } from './storage.js';

export function getFromStorageByKey(key, type) {
    return getValueByKey(key, type);
}

export function getFromStorageByKeys(keys, type) {
    if (keys) {
        Object.keys(keys).forEach((key) => {
            keys[key] = getValueByKey(key, type);
        });
    }

    return keys;
}

export function setToStorageByKey(key, value, isRemove, type) {
    if (isRemove) {
        removeByKey(key, type);
    }

    setValueByKey(key, value, type);
}

export function setToStorageByKeys(keys, removeKeys, type) {
    if (removeKeys) {
        Object.keys(removeKeys).forEach((key) => {
            removeByKey(removeKeys[key], type);
        });
    }

    if (keys) {
        Object.keys(keys).forEach((key) => {
            setValueByKey(key, keys[key], type);
        });
    }
}

export function removeFromStorageByKey(key, type) {
    removeByKey(key, type);
}

export function removeFromStorageByKeys(keys, type) {
    console.log(keys);
    if (keys) {
        Object.keys(keys).forEach((key) => {
            removeByKey(keys[key], type);
        });
    }
}

export function removeAllStorageKeys(type) {
    removeAllKeys();
}