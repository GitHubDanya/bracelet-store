<script>
    import { t } from '$lib/i18n.svelte';

    let { data } = $props();
    let bracelet = $state(null);
    let selectedIndex = $state(0);

    $effect(() => {
        if (!data.id) return;

        fetch(`/api/inventory/${data.id}`)
            .then(res => res.ok ? res.json() : null)
            .then(result => {
                bracelet = result;
                selectedIndex = 0;
            });
    });
</script>

<svelte:head>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Huninn&display=swap" rel="stylesheet">
</svelte:head>

<a class="return-button" href="/bracelets">&lt; return</a>

{#if bracelet}
    <div class="content">
        <div class="preview">
            <img class="thumbnail square-crop" src="{bracelet.thumbnailUrls[selectedIndex]}"/>

            <div class="toggle-bar">
                {#each bracelet.thumbnailUrls as image, index}
                    <button class="thumb-button"
                            class:active={selectedIndex === index}
                            on:click={() => selectedIndex = index}
                    >
                        <img class="square-crop" src="{image}"/>
                    </button>
                {/each}
            </div>
        </div>

        <div class="properties">
            <h2 class="title">
                {t(bracelet.name)}
            </h2>

            <p class="materials">
                {bracelet.materials.map(m => t(m)).join(', ')}
            </p>

            <p class="price">
                {bracelet.price} ₪
            </p>

            <button class="purchase-btn-dark">
                Pay with Bit
            </button>
        </div>
    </div>
{/if}

<style>
    .content {
        display: flex;
        flex-direction: column;
        gap: 15px;
    }
    
    .return-button {
        all: unset;
        font-family: "Huninn", sans-serif;
        cursor: pointer;
        font-size: 0.8rem;
        color: #696969;
    }
    
    .preview {
        display: flex;
        flex-direction: column;
    }
    
    .toggle-bar {
        display: flex;
        flex-direction: row;
        gap: 5px;
    }
    
    .thumbnail {
        width: calc(100% - 20px);
        height: auto;
        margin: 10px;
    }

    .square-crop {
        width: 100%;
        aspect-ratio: 1 / 1;
        object-fit: cover;
        object-position: center;
    }

    .thumb-button {
        border: 1px solid transparent;
        padding: 0;
        background: none;
        cursor: pointer;
        border-radius: 6px;
        overflow: hidden;
        opacity: 0.6;
        transition: opacity 0.2s, border-color 0.2s;
    }
    
    .thumb-button img {
        width: 60px;
        height: auto;
    }

    .thumb-button.active {
        border-color: #000;
        opacity: 1;
    }
    
    .properties {
        display: flex;
        flex-direction: column;
    }
    
    .title {
        font-size: 1.2rem;
    }
    
    .materials {
        font-size: 0.8rem;
        color: #696969;
    }
    
    .price {
        font-family: "Huninn", sans-serif;
        font-size: 100;
        text-align: right;
        color: #696969;
    }

    .purchase-btn-dark {
        background-color: #121212;
        color: #ffffff;
        font-size: 1rem;
        font-weight: 600;
        padding: 12px 24px;
        margin-top: 5px;
        border: none;
        border-radius: 8px;
        cursor: pointer;
        width: 100%;
    }

    .purchase-btn-dark:hover {
        background-color: #2a2a2a;
    }

    @media (min-width: 800px) {
        .content {
            flex-direction: row;
            margin: 0 auto;
            width: 100%;
            max-width: 70vw;
        }
        
        .preview {
            flex-direction: row-reverse;
            min-width: 0;
        }
        
        .thumbnail {
            max-width: 50vw;
            height: auto;
        }
        
        .properties {
            width: 20vw;
        }

        .toggle-bar {
            flex-direction: column;
        }
        
        .thumbnail {
            width: 600px;
        }
    }
</style>