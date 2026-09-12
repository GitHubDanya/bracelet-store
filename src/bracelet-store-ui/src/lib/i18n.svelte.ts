import { page } from '$app/state';

export function t(localizedObject: Record<string, string> | undefined): string {
    if (!localizedObject) return '';
    const lang = page.data.lang ?? 'en';
    return localizedObject[lang] ?? localizedObject['en'] ?? '';
}