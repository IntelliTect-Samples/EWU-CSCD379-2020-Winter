<template>
    <div>
        <div class="field">
            <label class="label">Title</label>
            <div class="control">
                <input class="input" type="text" v-model="clonedGift.title" />
            </div>
        </div>
        <div class="field">
            <label class="label">Description</label>
            <div class="control">
                <input class="input" type="text" v-model="clonedGift.description" />
            </div>
        </div>
        <div class="field">
            <label class="label">Url</label>
            <div class="control">
                <input class="input" type="text" v-model="clonedGift.url" />
            </div>
        </div>
        <div class="field">
            <label class="label">User ID</label>
            <div class="control">
                <input class="input" type="text" v-model="clonedGift.userId" />
            </div>
        </div>
        <div class="field is-grouped">
            <div class="control">
                <button id="submit" class="button is-primary" @click.once="saveGift">Submit</button>
            </div>
            <div class="control">
                <a class="button is-light" @click="cancelEdit">Cancel</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Vue, Component, Prop, Emit } from 'vue-property-decorator';
    import { Gift, GiftClient } from '../../secretsanta-client.g';
    @Component
    export default class GiftDetailsComponent extends Vue {
        @Prop()
        gift: Gift;
        clonedGift: Gift = <Gift>{};

        constructor() {
            super();
        }

        mounted() {
            let tempGift = { ...this.gift };
            this.clonedGift = <Gift>tempGift;
        }

        @Emit('gift-saved')
        async saveGift() {
            let giftClient = new GiftClient();
            if (this.clonedGift.id > 0) {
                await giftClient.put(this.clonedGift.id, this.clonedGift);
            }
            else {
                await giftClient.post(this.clonedGift);
            }
        }

        @Emit('gift-saved')
        cancelEdit() {

        }
    }
</script>