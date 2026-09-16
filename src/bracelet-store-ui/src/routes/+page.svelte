<script>
    import { onMount } from 'svelte';
    import { t } from '$lib/i18n.svelte';
    import { QueryResult } from '\$lib/query';

    let newBracelets = $state([]);
    let randomBracelets = $state([]);
    
    onMount(async () => {
        QueryResult.fetch('/api/inventory', (res) => newBracelets = res, { pageSize: 5 })
        QueryResult.fetch('/api/inventory', (res) => randomBracelets = res)
    });
</script>

<svelte:head>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=IBM+Plex+Sans+Devanagari:wght@100;200;300;400;500;600;700&display=swap" rel="stylesheet">
</svelte:head>

<div class="title">
    Elegant, handmade accessories
</div>

<p class="store-row-title">Bracelets</p>
<div class="store-row">
    {#each randomBracelets as item (item.id)}
        <a class="item" href="/bracelet/{item.id}">
            <img class="thumbnail" src="{item.thumbnailUrls[0]}" />
            <h2>{t(item.name)}</h2>
            <p class="description">
                {item.materials.map(m => t(m)).join(', ')}
            </p>
        </a>
    {/each}
</div>

<img class="banner" src="/images/braceletBanner.jpg" />

<p class="store-row-title">New Items</p>
<div class="store-row">
    {#each newBracelets as item (item.id)}
        <a class="item" href="/bracelet/{item.id}">
            <img class="thumbnail" src="{item.thumbnailUrls[0]}" />
            <h2>{t(item.name)}</h2>
            <p class="description">
                {item.materials.map(m => t(m)).join(', ')}
            </p>
        </a>
    {/each}
</div>

<style>
    :global(body) {
        margin: 0 !important;
    }
    
    :global(main)
    {
        overflow-x: hidden;
    }
    
    .title {
        display: flex;
        justify-content: center;
        align-items: center;
        font-weight: 200;
        font-size: 3rem;
        text-align: center;
        height: calc(100vh - 100px);
    }
    
    @media(min-width: 1024px) {
        .title {
            margin: 300px 0;
            height: auto;
        }
    }
    
    .store-row-title {
        margin-top: 10vh;
    }
    
    .store-row {
        display: flex;
        overflow-x: auto;
        justify-content: safe center;
        padding: 15px 0;
        gap: 10px;
    }
    
    @media(min-width: 800px) {
        .store-row-title {
            text-align: center;
        }
    }
    
    .store-row .item {
        all: unset;
        cursor: pointer;
        display: flex;
        flex: 0 0 150px;
        flex-direction: column;
        border: #808080 1px solid;
        justify-content: start;
        align-items: center;
        padding: 10px;
        width: 150px;
        transition: transform 0.3s ease-in-out;
    }

    .store-row .item:hover {
        transform: scale(1.05);
    }
    
    @media(min-width: 1024px) {
        .store-row .item {
            flex: 0 0 300px;
            width: 300px;
        }
    }
    
    .store-row .item h2 {
        font-size: 0.9rem;
        font-weight: 600;
        margin-top: 1rem;
        text-align: left;
        align-self: start;
    }
    
    .store-row .item .thumbnail,
    .store-row .item .thumbnail img {
        width: 100%;
        max-width: 100%;
        height: auto;
        display: block;
        object-fit: cover;
    }
    
    .store-row .item .description {
        font-size: 0.6rem;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
        align-self: start;
        margin-top: 0;
        width: 100%;
        color: #696969;
    }
    
    .banner {
        width: 100vw;
        position: relative;
        left: 50%;
        right: 50%;
        margin: 30vh -50vw 25vh;
        overflow-x: hidden;

        display: block;
        max-width: none;
        height: auto;
    }
</style>