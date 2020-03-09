<script>
    import {onMount} from 'svelte'

    let posts = []
    onMount(async x => {
        let q = await fetch(`/api/mols/${mol.MOL_ID}/posts`);
        posts = q.json();
    })
</script>






