<script>
    import {onMount} from 'svelte'

    export let mol = null;

    let posts = []
    onMount(async x => {
        let q = await fetch(`/api/mols/posts`);
        posts = await q.json();
    })
</script>

<ul class="list-inline">
{#each posts as item}
    <li class="list-inline-item">{item.NAME}</li>
{/each}
</ul>
